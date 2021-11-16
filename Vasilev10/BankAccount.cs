using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasilev10
{
    internal class BankAccount
    {
        public static int id = 1;

        private int accountNumber;
        private int balance;
        private AccountType type;

        public BankAccount()
        {
            accountNumber = id;
            idPlus();
        }

        public void PutMoney(int count){balance += count;}

        public bool TryTakeMoney(int count)
        {
            if (count > balance | count < 0) return false;
            else {balance -= count;return true;}
        }

        private static void idPlus() {id++;}
        public int GetBalance() {return balance;}
        public AccountType GetType() {return type;}
        public int GetNumber() {return accountNumber;}
        public void ChangeBalance(int money) {this.balance = money;}
        public void ChangeType(AccountType type) {this.type = type;}

        public static bool TryMoneyTransfer(ref BankAccount accountFrom, ref BankAccount accountTo, int money)
        {
            if (accountFrom.GetNumber() != accountTo.GetNumber())
            {
                if (accountFrom.TryTakeMoney(money))
                {
                    accountTo.PutMoney(money);
                    return true;
                }
                else
                    return false;
            }
            else 
                return false;
        }
    }
}
