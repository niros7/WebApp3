namespace WebApplication.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagTitle { get; set; }

        public override bool Equals(object obj)
        {
            var otherTag = (Tag)obj;
            return otherTag != null && TagTitle == otherTag.TagTitle;
        }

        public override int GetHashCode()
        {
            return $"{TagTitle}".GetHashCode();
        }
    }
}