using System;

class BanCo
{

    //Luôn có giá trị ở ô A:0;
    public int[,] banCoBiPha { get; set; }
    public int[,] banCoDaGiai { get => GiaiBanCo(); }
    private int congSai_DuDoan_1 { get; set; } = 0;
    private int congSai_DuDoan_2 { get; set; } = 0;
    private int soLanQuay { get; set; } = 0;

    public BanCo(int[,] banCoBiPha)
    {
        this.banCoBiPha = banCoBiPha;
    }

    // In bàn cờ ra
    public void InBanCo(int[,] banCo)
    {
        for (int i = 0; i < banCo.GetLength(0); i++)
        {
            for (int j = 0; j < banCo.GetLength(1); j++)
            {
                Console.Write($"{banCo[i, j]}\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    //Đảo bàn cờ
    private int[,] QuayBanCo(int[,] banCo)
    {
        int rows = banCo.GetLength(0);
        int cols = banCo.GetLength(1);

        int[,] rotated = new int[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                rotated[cols - j - 1, i] = banCo[i, j];
            }
        }
        return rotated;
    }


    //kiểm tra bài toán có giải được không
    private bool KiemTraBanCo()
    {
        int check = 0;
        while (check < 2)
        {
            int current = 0;
            bool first = true;
            foreach (var next in this.banCoBiPha)
            {
                if (!first)
                {
                    if (next == 0 || current == 0)
                    {

                    }
                    else
                    {
                        if (next > current)
                        {
                            congSai_DuDoan_1 = next - current;
                            congSai_DuDoan_2 = ((next - 1) * (next - 1)) - current;
                            this.soLanQuay = check;
                            return true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    first = false;
                }
                (current) = (next);
            }
            this.banCoBiPha = QuayBanCo(this.banCoBiPha);

            this.banCoBiPha = QuayBanCo(this.banCoBiPha);

            check += 1;
        }
        return false;
    }

    // trả về bàn cờ dự đoán
    public int[,] BanCoDuDoan(int[,] banCo, int congSai)
    {
        var banCoThuNghiem = (int[,])banCo.Clone();
        int prev = 0;
        bool first = true;
        for (int i = 0; i < banCoThuNghiem.GetLength(0); i++)
        {
            for (int j = 0; j < banCoThuNghiem.GetLength(1); j++)
            {
                if (!first)
                {
                    if (banCoThuNghiem[i, j] == 0)
                    {
                        int tempAnswer = prev + congSai;
                        int after = (int)Math.Sqrt(tempAnswer);
                        if (after * after == tempAnswer)
                        {
                            banCoThuNghiem[i, j] = after + 1;
                        }
                        else
                        {
                            banCoThuNghiem[i, j] = tempAnswer;
                        }
                    }
                }
                else
                {
                    first = false;
                }
                prev = banCoThuNghiem[i, j];
            }
        }
        return banCoThuNghiem;
    }

    // kiểm tra bàn cờ có đúng hay không
    public bool KiemTraDuDoan(int[,] TH, int congSai)
    {
        int prev = 0;
        bool first = true;
        foreach (var current in TH)
        {
            if (!first)
            {
                if (current - prev == congSai || (current - 1) * (current - 1) - prev == congSai)
                {
                }
                else
                {
                    return false;
                }
            }
            else
            {
                first = false;
            }
            prev = current;
        }
        return true;
    }

    // Giải bàn cờ 
    public int[,] GiaiBanCo()
    {
        // Kiểm tra bàn cờ có thể giải hay không nếu không thì trả về 0
        if (KiemTraBanCo())
        {

            /* Công sai của bàn cờ có thể có 2 trường hợp
            1: trường hợp  -- công sai = số u2 - u1 
            2: trường hợp  -- công sai = số (u2-1)*(u2-1) - u1 
            
            */
            var checking = false;
            var TH1 = BanCoDuDoan(this.banCoBiPha, congSai_DuDoan_1);
            checking = KiemTraDuDoan(TH1, congSai_DuDoan_1);
            // nếu kiểm tra dự đoán mà trả về sai thì sử dụng công sai dự đoán 2
            if (checking)
            {
                return TH1;
                // Đúng thì trả về dự đóan 1
            }
            else
            {
                var TH2 = BanCoDuDoan(this.banCoBiPha, congSai_DuDoan_2);
                checking = KiemTraDuDoan(TH2, congSai_DuDoan_2);
                return TH2;
                // Đúng thì trả về dự đóan 2
            }
        }
        else
        {
            Console.WriteLine($"{0} thể giải");
            return this.banCoBiPha;
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        // Bàn cờ 1
        BanCo bc_1 = new BanCo(new int[,]{
                {0 ,10, 5},
                {11, 0, 20},
                {15, 10 ,5}});

        // Bàn cờ 2
        BanCo bc_2 = new BanCo(new int[,]{
                {8 ,0, 6},
                {5, 15, 14},
                {13, 0 ,11}});

        // Bàn cờ 3
        BanCo bc_3 = new BanCo(new int[,]{
                {0 ,2, 0},
                {0, 0, 4},
                {0, 15 ,0}});

        //Bàn cờ 4
        BanCo bc_4 = new BanCo(new int[,]{
                {2 ,0, 5, 7},
                {0, 0, 0, 0},
                {0, 0, 0,0},
                {0, 0, 0,0}});

        //Bàn cờ 5
        BanCo bc_5 = new BanCo(new int[,]{
                {0 ,0, 0, 0},
                {0 ,0, 0, 0},
                {0, 0, 0, 0},
                {7, 5, 3,2}});

        //Ban Cờ 6
        BanCo bc_6 = new BanCo(new int[,]{
                {0 , 0},
                {3, 0}});

        // Ban Cờ 7
        BanCo bc_7 = new BanCo(new int[,]{
                {0 , 0,0,0,0},
                {0 , 0,0,0,0},
                {0 , 0,0,0,0},
                {0 , 0,0,0,0},
                {2,1,4,7,1}});



        bc_1.InBanCo(bc_1.banCoDaGiai);
        bc_2.InBanCo(bc_2.banCoDaGiai);
        // bc_3.InBanCo(bc_3.banCoDaGiai);
        // bc_4.InBanCo(bc_4.banCoDaGiai);
        // bc_5.InBanCo(bc_5.banCoDaGiai);
        // bc_6.InBanCo(bc_6.banCoDaGiai);
        // bc_7.InBanCo(bc_7.banCoDaGiai);
    }
}
