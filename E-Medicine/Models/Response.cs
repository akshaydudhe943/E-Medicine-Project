namespace E_Medicine_Backend.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public List<Users> ListUsers { get; set; }
        public Users User { get; set; }

        public List<Medicines> ListMedicine { get; set; }
        public Medicines medicine { get; set; }

        public List<Cart> ListCart { get; set; }

        public List<Orders> ListOrders { get; set; }
        public Orders order { get; set; }

        public List<OrderItems> ListItems { get; set; }
        public OrderItems orderItems { get; set; }
    }
}
