namespace OMF.ReviewManagementService.Command.Service.Command
{
    public class ReviewCommand : ServiceBus.Abstractions.Command
    {
        public string Rating { get; set; }
        public string Comments { get; set; }
        public int TblRestaurantId { get; set; }
        public int CustomerId { get; set; }
    }
}