// Funções utilitárias simples para as views
async function login(event){
  event.preventDefault();
  const email = document.getElementById('email').value;
  const password = document.getElementById('password').value;
  // Exemplo: chamar API de autenticação
  try{
    // substituir URL pela rota real de autenticação
    const res = await fetch('/v1/auth/login',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({email,password})});
    if(res.ok){
      alert('Login bem sucedido (demo)');
      window.location.href = 'dashboard.html';
    } else {
      const txt = await res.text();
      alert('Falha no login: '+txt);
    }
  }catch(e){
    console.error(e);
    alert('Erro ao contatar API (demo)');
  }
}

async function register(event){
  event.preventDefault();
  clearFormMessage();
  const name = document.getElementById('r-name').value.trim();
  const email = document.getElementById('r-email').value.trim();
  const password = document.getElementById('r-password').value;
  const passwordConfirm = document.getElementById('r-password-confirm').value;
  const role = document.getElementById('r-role').value;

  // Validações simples
  if(!name || !email || !password){
    showFormMessage('Preencha todos os campos obrigatórios');
    return;
  }
  if(password.length < 8){
    showFormMessage('A senha deve ter no mínimo 8 caracteres');
    return;
  }
  if(password !== passwordConfirm){
    showFormMessage('As senhas não conferem');
    return;
  }

  const btn = document.getElementById('register-btn');
  btn.disabled = true;
  btn.textContent = 'Registrando...';

  try{
    const res = await fetch('/v1/auth/register',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({name,email,password,role})});
    if(res.ok){
      showFormMessage('Registro realizado com sucesso', true);
      setTimeout(()=> window.location.href='login.html', 1200);
    } else {
      const txt = await res.text();
      showFormMessage('Falha no registro: ' + txt);
    }
  }catch(e){
    console.error(e);
    showFormMessage('Erro ao contatar API');
  }finally{
    btn.disabled = false;
    btn.textContent = 'Registrar';
  }
}

// Dashboard: renderiza gráfico simples com Chart.js
function renderActiveEmployeesChart(data){
  const ctx = document.getElementById('activeChart').getContext('2d');
  // esperar que Chart esteja carregado
  if(typeof Chart === 'undefined') return;
  new Chart(ctx,{
    type:'bar',
    data:{labels:data.labels, datasets:[{label:'Funcionários em Atividade',data:data.values,backgroundColor:'rgba(255,140,0,0.9)'}]},
    options:{plugins:{legend:{display:false}},scales:{y:{beginAtZero:true}}}
  });
}

// Exemplo de fetch para dados do gráfico
async function loadActiveEmployees(){
  try{
    // substituir pela rota real
    // const res = await fetch('/v1/person/activeStats');
    // const json = await res.json();
    const demo = {labels:['Jan','Fev','Mar','Abr','Mai','Jun'], values:[12,15,11,18,20,17]};
    renderActiveEmployeesChart(demo);
  }catch(e){console.error(e)}
}

// Inicialização comum
document.addEventListener('DOMContentLoaded',()=>{
  const loginForm = document.getElementById('login-form');
  if(loginForm) loginForm.addEventListener('submit',login);
  const regForm = document.getElementById('register-form');
  if(regForm) regForm.addEventListener('submit',register);
  // Monitor de força de senha
  const pwd = document.getElementById('r-password');
  const pwdStrength = document.getElementById('pwd-strength');
  if(pwd && pwdStrength){
    pwd.addEventListener('input',()=>{
      const score = passwordStrengthScore(pwd.value);
      const map = ['Fraca','Média','Forte','Muito forte'];
      pwdStrength.textContent = score === 0 ? '-' : map[Math.min(score-1,map.length-1)];
    });
  }
  const chartCanvas = document.getElementById('activeChart');
  if(chartCanvas) loadActiveEmployees();
});

function showFormMessage(msg, success){
  const el = document.getElementById('form-messages');
  if(!el) return;
  el.textContent = msg;
  el.style.color = success ? '#9fff9f' : '#ffcc80';
}

function clearFormMessage(){
  const el = document.getElementById('form-messages'); if(el) el.textContent = '';
}

function passwordStrengthScore(pwd){
  if(!pwd) return 0;
  let score = 0;
  if(pwd.length >= 8) score++;
  if(/[A-Z]/.test(pwd) && /[a-z]/.test(pwd)) score++;
  if(/\d/.test(pwd)) score++;
  if(/[^A-Za-z0-9]/.test(pwd)) score++;
  return Math.min(score,4);
}
