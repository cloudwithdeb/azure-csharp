using UsersModelNSP;

namespace UsersRepositoryNSP
{
    public static class UsersRepository
    {
        public static List<UsersModel> usr = new() 
        {
            // First User Data
            new() {
                Id="1010010101",
                usersid=10,
                username="Owusu",
                details = new Details{
                    email="owusu@gmail.com",
                    location="Kasoa Bignam Town",
                    age=99
                },
                others = new List<Others> {
                    new() {
                        mom="Georgina Owusu",
                        dad="Benard Owusu"
                    },
                    new() {
                        mom="Georgina Owusu",
                        dad="Benard Owusu"
                    }
                }
            },

            // Second User Data
            new() {
                Id="2020202020",
                usersid=20, 
                username="Eva Pokuah", 
                details = new Details{
                    location="Tema Moto way",
                    email="eva@gmail.com",
                    age=99
                },
                others = new List<Others> {
                    new() {
                        mom="Joyce Blessing",
                        dad="Opoku Mensah"
                    }
                }
            }
             
        };
    }
}