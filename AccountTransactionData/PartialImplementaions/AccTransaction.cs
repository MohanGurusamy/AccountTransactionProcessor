using System;

namespace AccountTransactionData
{
    public partial class AccTransaction : IEquatable<AccTransaction>
    {
        public bool Equals(AccTransaction other)
        {
            if(other == null)
                return false;

            return this.Account == other.Account
                && this.CurrencyCode == other.CurrencyCode
                && this.Description == other.Description
                && this.Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AccTransaction);
        }

        public override int GetHashCode()
        {
            return string.Format("{0}:{1}:{2}:{3}", Account, CurrencyCode, Description, Value).GetHashCode();
        }
    }
}
