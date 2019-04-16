namespace mService.Controllers
{
    public class mNews
    {

        public string Title { get; set; }
        public string Date { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }


    public class mNotifications
    {
        public mNotifications()
        {
        }

        public string Date { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public int isSeen { get; set; }
        public string Title { get; set; }
    }
}