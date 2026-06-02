Console.WriteLine("Hello, World!");
Solution sln = new Solution();
sln.TwoSum(new int[] { 2, 7, 11, 15 }, 9);
public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        int[] result = new int[2];
        int sumtotal = 0;
        while (sumtotal != target)
        {
            int resultatual = 0;
            for (int i = 0; i < nums.Length - 1; i++ )
            {
                resultatual = target - result[i];
                if (result.Contains(resultatual))
                {
                    result[0] = result.IndexOf(result[i]);
                    result[1] = result.IndexOf(resultatual);
                    sumtotal = result[i] + resultatual;
                    break;
                }

            }
        }
        return result;
    }
}
