



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            User[] client = new User[1];
            Card card = new Card();
            OperHistory operHistory = new OperHistory(new DateTime[100], new string[100]);
            int historyCount = 0;

            int oper = 0;
            List<User> user = new List<User>
{
new User(){Name="Kamran",Surname="Aliyev",CreditCard= new Card(){PAN="1234567891234567",PIN="1234",CVC="123",ExpireDate="03/19", Balance=1000} },
new User(){Name="Orxan",Surname="Hasanov",CreditCard= new Card(){PAN="9876543219876543",PIN="4123",CVC="321",ExpireDate="09/22",Balance=2500} },
new User(){Name="Murad",Surname="Aliyev",CreditCard= new Card(){PAN="1234567899876543",PIN="4321",CVC="258",ExpireDate="01/21",Balance=1600} },
new User(){Name="Ilqar",Surname="Tagiyev",CreditCard= new Card(){PAN="9638527411472583",PIN="9874",CVC="745",ExpireDate="06/23",Balance=13000} },
new User(){Name="Farhad",Surname="Hacinski",CreditCard= new Card(){PAN="1478523699632587",PIN="1104",CVC="735",ExpireDate="07/20",Balance=6000} },
};

            Console.WriteLine("PIN-i daxil edin");

            card.Pin(ref client, ref user);

            Console.WriteLine();
            while (true)
            {
                card.CardOperation(ref client, ref user, ref oper, card, ref operHistory, ref historyCount);
                Console.WriteLine("PIN-i daxil edin");
                card.Pin(ref client, ref user);
            }

        }
    }

    class Card
    {
        public string PAN { get; set; }
        public string PIN { get; set; }
        public string CVC { get; set; }
        public string ExpireDate { get; set; }
        public decimal Balance { get; set; }

        public void Pin(ref User[] client, ref List<User> user)
        {
            for (int i = 0; i < 3; i++)
            {
                var Pin = Console.ReadLine();
                client = user.Where(x => x.CreditCard.PIN == Pin).ToArray();
                try
                {
                    Console.WriteLine($"{client[0].Name} {client[0].Surname}, xosh gelmisiniz. Zehmet olmasa ashagidakilardan birini sechesiniz");
                    i = 3;
                }
                catch (Exception)
                {
                    Console.WriteLine("Pin dogru deyil");
                    if (i == 2)
                    {
                        Console.WriteLine("Kartiniz bloklashdirilmishdir. Bankiniza muraciet edin");
                        Environment.Exit(0);
                    }
                }
            }
        }

        public void ShowMenu(ref User[] client, ref List<User> user, ref int oper)
        {
            Console.WriteLine("1. Balans");
            Console.WriteLine("2. Nagd pul");
            Console.WriteLine("3. Edilen emeliyyatlarin siyahisi");//Bunu etmek qalir
            Console.WriteLine("4. Cart to Cart"); //bunu etmek qalir

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    oper = Convert.ToInt32(Console.ReadLine());
                    if (oper == 1 || oper == 2 || oper == 3 || oper == 4)
                    {
                        i = 2;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Sehv kod");
                        Console.WriteLine();
                        Console.WriteLine("1. Balans");
                        Console.WriteLine("2. Nagd pul");
                        Console.WriteLine("3. Edilen emeliyyatlarin siyahisi");
                        Console.WriteLine("4. Cart to Cart");
                        i = 0;
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Sehv kod");
                    Console.WriteLine();
                    Console.WriteLine("1. Balans");
                    Console.WriteLine("2. Nagd pul");
                    Console.WriteLine("3. Edilen emeliyyatlarin siyahisi");
                    Console.WriteLine("4. Cart to Cart");
                    i = 0;
                }
            }
        }

        public void CardOperation(ref User[] client, ref List<User> user, ref int oper, Card card, ref OperHistory operHistory, ref int historyCount)
        {

            decimal balance = client[0].CreditCard.Balance;
            for (int g = 0; g < 3; g++)
            {

                ShowMenu(ref client, ref user, ref oper);

                switch (oper)
                {
                    case 1:
                        operHistory.opertime[historyCount] = new DateTime();
                        operHistory.opertype[historyCount] = "Balans emeliyyati";
                        operHistory.opertime[historyCount++] = DateTime.Now;

                        Console.WriteLine($"Balansiniz: {balance} AZN");

                        g = 0; break;

                    case 2:
                        operHistory.opertime[historyCount] = new DateTime();
                        operHistory.opertype[historyCount] = "Nagd pul emeliyyati";
                        operHistory.opertime[historyCount++] = DateTime.Now;

                        Console.WriteLine("1. 10 AZN");
                        Console.WriteLine("2. 20 AZN");
                        Console.WriteLine("3. 50 AZN");
                        Console.WriteLine("4. 100 AZN");
                        Console.WriteLine("5. Diger");

                        int cashcode = 0;
                        for (int i = 0; i < 2; i++)
                        {
                            try
                            {
                                cashcode = Convert.ToInt32(Console.ReadLine());
                                if (cashcode == 1 || cashcode == 2 || cashcode == 3 || cashcode == 4 || cashcode == 5)
                                {
                                    i = 2;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Sehv sechim");
                                    Console.WriteLine("1. 10 AZN");
                                    Console.WriteLine("2. 20 AZN");
                                    Console.WriteLine("3. 50 AZN");
                                    Console.WriteLine("4. 100 AZN");
                                    Console.WriteLine("5. Diger");
                                    i = 0;
                                }

                            }
                            catch (Exception)
                            {
                                Console.Clear();
                                Console.WriteLine("Sehv sechim");
                                Console.WriteLine("1. 10 AZN");
                                Console.WriteLine("2. 20 AZN");
                                Console.WriteLine("3. 50 AZN");
                                Console.WriteLine("4. 100 AZN");
                                Console.WriteLine("5. Diger");
                                i = 0;
                            }
                        }

                        switch (cashcode)
                        {
                            case 1: balance -= 10; client[0].CreditCard.Balance = balance; return;
                            case 2: balance -= 20; client[0].CreditCard.Balance = balance; return;
                            case 3: balance -= 50; client[0].CreditCard.Balance = balance; return;
                            case 4: balance -= 100; client[0].CreditCard.Balance = balance; return;
                            case 5:
                                int myCash = 0;
                                for (int i = 0; i < 2; i++)
                                {

                                    Console.WriteLine("Meblegi daxil edin");

                                    try
                                    {
                                        myCash = Convert.ToInt16(Console.ReadLine());
                                        if (myCash <= 0)
                                        {
                                            Console.WriteLine("Menfi ve ya 0 daxil etmek olmaz");
                                            i = 0;
                                        }
                                        else
                                        {
                                            i = 2;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        Console.WriteLine("Yalniz eded daxil etmelisiniz");
                                        i = 0;
                                    }

                                }

                                if (balance > myCash)
                                {

                                    balance -= myCash; client[0].CreditCard.Balance = balance; return;
                                }
                                else
                                {
                                    g = 2;
                                    Console.WriteLine("Balansinizda hemin mebleg yoxdur"); break;

                                }
                        }
                        break;

                    case 3:
                        for (int i = 0; i < historyCount; i++)
                        {
                            Console.WriteLine($"{operHistory.opertype[i]}: {operHistory.opertime[i]}");
                        }
                        break;

                    case 4:
                        operHistory.opertime[historyCount] = new DateTime();
                        operHistory.opertype[historyCount] = "Cart to Cart";
                        operHistory.opertime[historyCount++] = DateTime.Now;

                        Console.WriteLine("Pul kochurmek istediyiniz kartin PIN-i daxil edin");

                        var SecondPIN = Console.ReadLine();

                        var r = user.Where(x => x.CreditCard.PIN == SecondPIN);


                        foreach (var item in r)
                        {

                            Console.WriteLine($"Pul {item.Name} adli shexse kochuruldu"); return; //kochurulen meblegi daxil etmeyimizi task-da istemediyiniz uchun hemin hisseni yazmadim

                        }

                        Console.WriteLine("bu PIN koda aid kart tapilmadi");



                        break;

                }

            }
        }
    }

    class OperHistory
    {

        public DateTime[] opertime { get; set; }
        public string[] opertype { get; set; }

        public OperHistory(DateTime[] opertime, string[] opertype)
        {
            this.opertime = opertime;
            this.opertype = opertype;
        }
    }


    class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Card CreditCard { get; set; }
    }
}
