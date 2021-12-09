namespace Store.Extensions
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this List<T> list, Random r)
        {
            int shuffles = list.Count * 2;
            for(int i = 0; i < shuffles; i++)
            {
                int index1 = r.Next(0, list.Count);
                int index2 = r.Next(0, list.Count);

                T item1 = list[index1];
                T item2 = list[index2];

                list[index1] = item2;
                list[index2] = item1;
            }
        }
    }
}
