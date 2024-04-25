using Microsoft.Data.SqlClient;
using System.Data;

namespace E_Medicine_Backend.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@MobileNo", users.MobileNo);
            cmd.Parameters.AddWithValue("@Dob", users.Dob);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Registered Successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Registration failed";
            }


            return response;
        }

        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["user.Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is Valid";
                response.User = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is Invalid";
                response.User = null;
            }
            return response;
        }

        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", users.Id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["user.Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "User exist";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User dose not exist";
                response.User = users;
            }
            return response;
        }

        public Response updateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@MobileNo", users.MobileNo);
            cmd.Parameters.AddWithValue("@Dob", users.Dob);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Updated Successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User update failed";
            }
            return response;
        }

        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cart.UserId);
            cmd.Parameters.AddWithValue("@MedicineId", cart.MedicineId);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item Added Successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;
        }

        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_placeOrder", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", users.Id);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order Placed Successfully!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order Failed";
            }
            return response;
        }

        public Response orderList(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@Id", users.Id);

            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                    order.OrderNumber = Convert.ToString(dataTable.Rows[i]["OrderNumber"]);
                    order.OrderTotal = Convert.ToDecimal(dataTable.Rows[i]["OrderToatl"]);
                    order.OrderStatus = Convert.ToString(dataTable.Rows[i]["OrderStatus"]);
                    listOrder.Add(order);
                }
                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order Detailes Fetched";
                    response.ListOrders = listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order Detailse Not Available";
                    response.ListOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order Detailse Not Available";
                response.ListOrders = null;
            }
            return response;
        }

        public Response addUpdateMedicine(Medicines medicines, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_addUpdateMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", medicines.Name);
            cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
            cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
            cmd.Parameters.AddWithValue("@Diseaces", medicines.Diseaces);
            cmd.Parameters.AddWithValue("@ExpDate", medicines.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", medicines.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", medicines.Status);
            cmd.Parameters.AddWithValue("@Type", medicines.Type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine Added Successfully!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Medicine did not Saved, Try Again!";
            }
            return response;
        }

        public Response userList(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;


            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                    user.FirstName = Convert.ToString(dataTable.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dataTable.Rows[i]["LastName"]);
                    user.Password = Convert.ToString(dataTable.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dataTable.Rows[i]["Email"]);
                    user.MobileNo = Convert.ToString(dataTable.Rows[i]["MobileNo"]);
                    user.Dob = Convert.ToDateTime(dataTable.Rows[i]["Dob"]);
                    user.Fund = Convert.ToDecimal(dataTable.Rows[i]["Fund"]);
                    user.Type = Convert.ToString(dataTable.Rows[i]["Type"]);
                    user.Status = Convert.ToInt32(dataTable.Rows[i]["Status"]);
                    user.CreatedOn = Convert.ToDateTime(dataTable.Rows[i]["CreatedOn"]);

                    listUsers.Add(user);
                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Users Detailes Fetched";
                    response.ListUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Users Detailse Not Available";
                    response.ListUsers = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order Detailse Not Available";
                response.ListOrders = null;
            }
            return response;
        }
    }
}
