namespace BankSystem.Models
{
    public class UserAccountModel
    {
        public int AccountNumber 
        {
            get;
            set;
        }
        public int CustomerID
        {
            get;
            set;
        }
        public string? CustomerName
        {
            get;
            set;
        }
        public int AccountBalance
        {
            get;
            set;
        }
        public int DepositAmount
        {
            get;
            set;
        }

        public int WithdrawalAmount
        {
            get;
            set;
        }
    }
}
