namespace lab1
{
    public class Member
    {
        private string memberId;
        private string name;

        public Member(string memberId, string name)
        {
            this.memberId = memberId;
            this.name = name;
        }

        public override string ToString()
        {
            return memberId + " - " + name;
        }
    }
}