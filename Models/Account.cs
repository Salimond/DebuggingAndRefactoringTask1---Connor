using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebuggingAndRefactoringTask1.Models
{
    public class Account
    {
        public Account(string id, string name, double balance = 0) 
        {
            Id = id;
            Name = name;
            Balance = balance;
        }

        public string Id { get; }
        public string Name { get; }
        public double Balance { get; set; }
    }
}
