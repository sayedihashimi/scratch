using System;
namespace ConsoleCsharp8
{
    public interface IMessage
    {
        string DisplayMessage { get; set; }

        public string GetMessage()
        {
            return $"Message: {DisplayMessage}";
        }
    }
}
