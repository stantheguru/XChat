using System.ComponentModel.DataAnnotations;

namespace XChat.Models
{
    public class Chat
    {
        [Key]
        public int ChatID { get; set; }

        [Required]
        public string SenderID  { get; set; }

        [Required]
        public string RecipientID { get; set; }

        [Required]
        public string Message { get; set; }

       

        [Required]
        public DateTime SendDate { get; set; }

        [Required]
        public string Read { get; set; }
    }
}
