using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Renderers;
using DellyShopApp.Views.Pages;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DellyShopApp.Services
{
    public class DataService : IDisposable
    {
        static DataService _instance;

        public ObservableCollection<NotificationModel> NotificationList = new ObservableCollection<NotificationModel>();
        public ObservableCollection<ProductListModel> ProcutListModel = new ObservableCollection<ProductListModel>();
        public ObservableCollection<ProductListModel> BasketModel = new ObservableCollection<ProductListModel>();
        public ObservableCollection<OrderListModel> OrderModel = new ObservableCollection<OrderListModel>();


        public List<Category> CatoCategoriesList = new List<Category>();
        public List<Category> Carousel = new List<Category>();
        public List<StartList> StartList = new List<StartList>();
        public List<Category> CatoCategoriesDetail = new List<Category>();
        public List<CommentModel> CommentList = new List<CommentModel>();
        public List<ShopModel> ShopDetails = new List<ShopModel>();
        public ChangeUserData EditProfile = new ChangeUserData();
        public List<ChangeAddress> changeAddress = new List<ChangeAddress>();
        public List<CategoryDetailPage> Details = new List<CategoryDetailPage>();
        public List<VendorsOrder> vendors = new List<VendorsOrder>();
        public List<UserOrder> user = new List<UserOrder>();
        public OrderDetails orderdetails = new OrderDetails();
        public OrgData ObjOrgData = new OrgData();
        public Cart cart = new Cart();
        public Order order = new Order();
        public Login login = new Login();
        public Registration registration = new Registration();
        public Users_DTO users = new Users_DTO();
        public OrderCheckOut ordercheckout = new OrderCheckOut();
        public double TotalPrice = 0;
        
      
       
        public static DataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataService();
                }
                return _instance;
            }
        }
        public static void Restart()
        {

            _instance = null;
            Disposed = true;
        }
        protected static bool Disposed { get; private set; }
        public object Category { get; internal set; }
        public static object ItemsSource { get; internal set; }

        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public DataService()
        {

            NotificationList.Add(new NotificationModel
            {
                Title = AppResources.NotificatinTitle,
                SubTitle = AppResources.NotificationSubtitle,
                Description = AppResources.LoremIpsum,
                Id = 1,
                Image = "elecronics.jpeg",
                InstertedAt = DateTime.Now

            });

            NotificationList.Add(new NotificationModel
            {

                Title = AppResources.NotificatinTitle,
                SubTitle = AppResources.NotificationSubtitle,
                Description = AppResources.LoremIpsum,
                Id = 2,
                Image = "shoes.jpg",
                InstertedAt = DateTime.Now

            });
            ProcutListModel.Add(new ProductListModel
            {
                Title = AppResources.ProcutTitle,
                Brand = AppResources.ProductBrand,
                Id = 1,
                Image = "shoesBlack",
                Price = 362,
                VisibleItemDelete = false,
                ProductList = new string[] { "red1", "shoesBlack" },
                OldPrice = 570,
                orgId = 1,
                Quantity = 1,
                orderId = 1,

               
            });
            ProcutListModel.Add(new ProductListModel
            {
                Title = AppResources.ProcutTitle1,
                Brand = AppResources.ProductBrand1,
                Id = 2,
                Image = "grazy1",
                Price = 150,
                VisibleItemDelete = false,
                ProductList = new string[] { "garzy2", "grazy1" },
                OldPrice = 270,
                orgId = 1,
                Quantity = 1,
                orderId = 1
                
            });
            ProcutListModel.Add(new ProductListModel
            {
                Title = AppResources.ProcutTitle2,
                Brand = AppResources.ProductBrand2,
                Id = 3,
                Image = "shoesyellow",
                Price = 299,
                VisibleItemDelete = false,
                ProductList = new string[] { "py_1", "shoesyellow" },
                OldPrice = 400,
                orgId = 2,
                Quantity = 1,
                orderId = 1
                
            });
            vendors.Add(new VendorsOrder
            {
                orgId = 1,
                userId = 1,
                orderId = 123,
                Price = 555
            });
            vendors.Add(new VendorsOrder
            {
                orgId = 2,
                userId = 2,
                orderId = 111,
                Price = 500
            });
            vendors.Add(new VendorsOrder
            {
                orgId = 3,
                userId = 3,
                orderId = 222,
                Price = 600
            });
            vendors.Add(new VendorsOrder
            {
                orgId = 4,
                userId = 4,
                orderId = 333,
                Price = 999
            });
            orderdetails = new OrderDetails();
            OrderModel.Add(new OrderListModel
            {
                orgId = 1,
                UserId = 1,
               


            });
            OrderModel.Add(new OrderListModel
            {
                orgId = 1,
                UserId = 1,
              



            });
            OrderModel.Add(new OrderListModel
            {
                orgId = 1,
                UserId = 1,
               

            });
            OrderModel.Add(new OrderListModel
            {
                orgId = 1,
                UserId = 1,
                


            });
            orderdetails.ProductLists = OrderModel.ToList();
            //orderdetails.Date = "01/01/2001";
            orderdetails.Address = "c-807 Rajkot";
            ShopDetails = new List<ShopModel>();
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Grocery Store",
                OrgId = 1,
                Image = "Grocery_Store.png",
                
            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Pet Store",
                OrgId = 2,
                Image = "Pet_Store.png",
               
            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Coffee Store",
                OrgId = 3,
                Image = "Coffee_store.png",
               
            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Fruit Store",
                OrgId = 4,
                Image = "Fruit_Store.png",
                
            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Cloth Store",
                OrgId = 5,
                Image = "Cloth_Store.png",
               
            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Hardware Store",
                OrgId = 6,
                Image = "Hardware_Store.png",
               
            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Grocery Store",
                OrgId = 1,
                Image = "Grocery_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Pet Store",
                OrgId = 2,
                Image = "Pet_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Coffee Store",
                OrgId = 3,
                Image = "Coffee_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Fruit Store",
                OrgId = 4,
                Image = "Fruit_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Cloth Store",
                OrgId = 5,
                Image = "Cloth_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Hardware Store",
                OrgId = 6,
                Image = "Hardware_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Book Store",
                OrgId = 7,
                Image = "Book_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Coffee Store",
                OrgId = 8,
                Image = "Coffee_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Fruit Store",
                OrgId = 9,
                Image = "Fruit_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Cloth Store",
                OrgId = 10,
                Image = "Cloth_Store.png",

            });
            ShopDetails.Add(new ShopModel
            {
                ShopName = "Hardware Store",
                OrgId = 11,
                Image = "Hardware_Store.png",
            });


            CatoCategoriesList.Add(new Category
            {
                CategoryName = AppResources.Shoes,
                Banner = "shoesCategory.png",
                CategoryId = "1",
                orgID = 1

            });
            CatoCategoriesList.Add(new Category
            {
                CategoryName = AppResources.Electronics,

                Banner = "electronicCategory.png",
                CategoryId = "2",
                orgID = 1
            });
            CatoCategoriesList.Add(new Category
            {
                CategoryName = AppResources.Clothing,

                Banner = "clothingCategory.png",
                CategoryId = "3",
                orgID = 1
            });
            CatoCategoriesList.Add(new Category
            {
                CategoryName = AppResources.Shoes,
                Banner = "shoesCategory.png",
                CategoryId = "1",
                orgID = 2

            });
            CatoCategoriesList.Add(new Category
            {
                CategoryName = AppResources.Electronics,

                Banner = "electronicCategory.png",
                CategoryId = "2",
                orgID = 2
            });
            CatoCategoriesList.Add(new Category
            {
                CategoryName = AppResources.Clothing,

                Banner = "clothingCategory.png",
                CategoryId = "3",
                orgID = 2
            });
            

           





            login.email = "";
            login.password = "";
            login.org_Id = 1;
           

            cart.orgId = 1;
            cart.UserId = 1;

            order.orgId = 1;
            order.UserId = 1;

         
            ObjOrgData.ID = 1;
            ObjOrgData.Image = "logo.png";

         

            changeAddress.Add(new ChangeAddress
            {
                
                zipcode = "123",
                SelectCity = "Rajkot",
                SelectState = "1",
                AddressDetail = "abc",
               
                
            });

            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            CommentList.Add(new CommentModel
            {
                Name = "Ufuk Sahin",
                CommentTime = "12/1/19",
                Id = 1,
                Rates = StartList
            });
            CommentList.Add(new CommentModel
            {
                Name = "Hans Goldman",
                CommentTime = "11/6/19",
                Id = 2,
                Rates = StartList.Skip(0).ToList()
            });
            CommentList.Add(new CommentModel
            {
                Name = "Jon Goodman",
                CommentTime = "12/8/19",
                Id = 3,
                Rates = StartList.Skip(1).ToList()
            });
            CommentList.Add(new CommentModel
            {
                Name = "UfuK Zimmer",
                CommentTime = "12/8/20",
                Id = 3,
                Rates = StartList.Skip(1).ToList()
            });
            CatoCategoriesDetail.Add(new Category
            {
                Banner = "shoes.jpg",
                orgID = 1

            });
            CatoCategoriesDetail.Add(new Category
            {
                Banner = "bestShoes.jpg",
                orgID = 2
            });
            CatoCategoriesDetail.Add(new Category
            {
                Banner = "bestofYear.jpg",
                orgID = 3
            });
            CatoCategoriesDetail.Add(new Category
            {
                Banner = "shoes.jpg"
            });

            Carousel.Add(new Category
            {
                CategoryId = "1",  
                Banner = "shoes.jpg",
                orgID = 1
            });
            Carousel.Add(new Category
            {
                Banner = "clothing.jpg",
                orgID = 1,
                CategoryId = "2"
            });
            Carousel.Add(new Category
            {
                Banner = "elecronics.jpeg",
                CategoryId = "3",
                orgID = 2
            });
            Carousel.Add(new Category
            {
                Banner = "images.jpeg",
                CategoryId = "3",
                orgID = 2
            });
        }

      

        public static async Task<List<Category>> GetCategories(int orgId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Category/GetAllCategories?OrgId=" + orgId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Category>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
         public static async Task<List<ShopModel>> GetAllOrganizations()
        {
            try
            {
                // await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //"bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Organization/GetAllOrganizations?OrgId=1");

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ShopModel>>(result);
            }
            catch (Exception ex)
            {
                throw;

            }
        }
        public static async Task<List<ProductListModel>> GetMostSellerProductsByOrganizations(int orgId , int? UserId = null)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Products/GetMostSellerProductsByOrganizations?org_Id=" + orgId + "&UserID=" + UserId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static async Task<List<ProductListModel>> GetAllProductsByOrganizations(int orgId, int? UserId = null)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Products/GetAllProductsByOrganizations?org_Id=" + orgId + "&UserID=" + UserId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<List<ProductListModel>> GetLastVisitedProductsByOrganizations(int orgId, int? UserId = null)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Products/GetLastVisitedProductsByOrganizations?org_Id=" + orgId + "&UserID=" + UserId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<List<Category>> GetAllCategories(int orgId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Category/GetAllCategories?OrgId=" + orgId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Category>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<List<ChangeAddress>> GetAddressByUserId(int orgId,int userId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Cart/GetAddressByUserId?OrgId=" + orgId + "&UserId=" + userId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ChangeAddress>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<List<VendorsOrder>> GetOrderDetails(int orgId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Cart/GetOrderDetails?OrgId=" + orgId );

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<VendorsOrder>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<List<UserOrder>> GetMyOrderDetails(int orgId, int UserId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Cart/GetMyOrderDetails?OrgId=" + orgId + "&UserId=" + UserId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserOrder>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<OrderDetails> GetOrderDetailsByOrderMasterId(int orgId, int OrderMasterId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Cart/GetOrderDetailsByOrderMasterId?OrgId=" + orgId + "&OrderMasterId=" + OrderMasterId );

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderDetails>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<string> AddToCart(Cart cart)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(cart);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Cart/AddToCart", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<string> Submit(Orders orders)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(orders);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Cart/UpdateOrderStatus", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<string> Cancel(Cancelorder cancelorder)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler

                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(cancelorder);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Cart/CancelOrder", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<int> RemoveFromCart(int userId, int orgId, int proId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Cart/RemoveFromCart?userId=" + userId + "&proId=" + proId + "&orgId=" + orgId);
                string result = await response.Content.ReadAsStringAsync();
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<Users_DTO> Login(Login login)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(login);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Auth/UserLogin", c);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Users_DTO>(result);
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<string> Registration(Registration registration)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(registration);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/User/UserRegistration", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
      
        public static async Task<int> PlaceOrder(Order order)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                return 0;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<string> EditUserData(ChangeUserData changeUserData)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                var payload = JsonConvert.SerializeObject(changeUserData);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/User/UpdateUserProfile", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
         public static async Task<List<ChangeUserData>> GetUserById(int userId, int orgId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/User/GetUserById?org_id=" + orgId  + "&user_id=" + userId );
                
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ChangeUserData>>(result);
                //return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<string> Checkout(OrderCheckOut orderCheckOut)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                var payload = JsonConvert.SerializeObject(orderCheckOut);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Cart/PlaceOrder", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<string> MyFavourite(Favourite favourite)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(favourite);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Cart/AddtoFavourite", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<int> RemovefromFavourite(int proId, int userId, int orgId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Cart/RemovefromFavourite?ProductId=" + proId + "&UserId=" + userId + "&OrgId=" + orgId);

                string result = await response.Content.ReadAsStringAsync();
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<List<ProductListModel>> GetWishlistByUser(int orgId,int userId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Products/GetWishlistByUser?org_Id="+orgId+"&user_Id=" + userId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<List<ProductListModel>> GetAllCartDetails(int orgId, int userId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                AppSettings.ApiUrl + "api/Cart/GetAllCartDetails?OrgId=" + orgId + "&UserId=" + userId );

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

      
        public static async Task<List<ProductListModel>> SearchProducts(int orgId, string productname)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Products/SearchProducts?org_Id=" + orgId + "&productname=" + productname);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<int> Delete(DeleteItem deleteItem)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(deleteItem);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Cart/AddToCart", c);

                string result = await response.Content.ReadAsStringAsync();
                return 0;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<List<ProductListModel>> GetAllProductsByCategory(int orgId, int categoryId)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Products/GetAllProductsByCategory?orgId=" + orgId + "&CategoryId=" + categoryId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<string> ForgetPassword(ForgetPassword forget)
        {
            try
            {
                //await TokenValidator.CheckTokenValidity();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    "bearer", Preferences.Get("accessToken", string.Empty));
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var payload = JsonConvert.SerializeObject(forget);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/User/ForgetPassword", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)

            {
                throw;
            }
        }

    }
}

