namespace InvalidOperation.Api.Models
{
    public class ActionRule
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public ActionRuleParameters Parameters { get; set; }


    }
    public class ActionRuleParameters
    {
        public List<Action> Actions { get; set; }

    }

    public class Action
    {
        public EMail Email { get; set; }
        public Sms Sms { get; set; }

       
    }
    public class EMail
    {
        public string Message { get; set; }
    }
    public class Sms
    {
        public string Message { get; set; }
    }
}
