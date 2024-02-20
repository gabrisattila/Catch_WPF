using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catch_Wpf.Persistence
{
    public class DataAccess : IDataAcces
    {
        public GameTable Load(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line = reader.ReadLine();
                    int m = int.Parse(line);
                    int[,] t = new int[m, m];
                    GameTable table = new GameTable(m, t);
                    String[] nums;
                    for (int i = 0; i < m; i++)
                    {
                        line = reader.ReadLine();
                        nums = line.Split(' ');
                        for (int j = 0; j < m; j++)
                        {
                            table.Tábla[i, j] = int.Parse(nums[j]);
                            if (int.Parse(nums[j]) == 2) 
                            {
                                table.E1 = true;
                                table.E1Pos = new int[2];
                                table.E1Pos[0] = i;
                                table.E1Pos[1] = j;
                            }
                            if (int.Parse(nums[j]) == 4)
                            {
                                table.E2 = true;
                                table.E2Pos = new int[2];
                                table.E2Pos[0] = i;
                                table.E2Pos[1] = j;
                            }
                            if (int.Parse(nums[j]) == 3)
                            {
                                table.P = true;
                                table.PPos = new int[2];
                                table.PPos[0] = i;
                                table.PPos[1] = j;
                            }
                        }
                    }
                    return table;
                }
            }
            catch
            {
                throw new DataException();
            }
        }


        public async Task SaveAsync(string path, GameTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(table.M);
                    for (int i = 0; i < table.M; i++)
                    {
                        for (int j = 0; j < table.M; j++)
                        {
                            await writer.WriteAsync(table.Tábla[i, j] + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new DataException();
            }
        }
    }
}
