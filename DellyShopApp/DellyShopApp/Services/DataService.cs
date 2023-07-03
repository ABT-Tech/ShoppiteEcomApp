using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using DellyShopApp.Models;
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
        public List<CustomerInfo> customerInfo = new List<CustomerInfo>();
        public List<Report> report = new List<Report>();
        public List<Products> products = new List<Products>();
        public List<VendorsOrder> vendors = new List<VendorsOrder>();
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
            customerInfo.Add(new CustomerInfo
            {
                Username = "abcd",
                Email = "abc@gmail.com",
                icon = "Red1.jpeg", 
                Active =false
            }); customerInfo.Add(new CustomerInfo
            {
                Username = "xyz",
                Email = "xyz@gmail.com",
                icon = "Green.jpeg",
                Active = true
            }); customerInfo.Add(new CustomerInfo
            {
                Username = "opq",
                Email = "opq@gmail.com",
                icon = "Green.jpeg",
                Active = true

            });
            customerInfo.Add(new CustomerInfo
            {
                Username = "abcd",
                Email = "abc@gmail.com",
                icon = "Red1.jpeg",
                Active = false
            }); customerInfo.Add(new CustomerInfo
            {
                Username = "xyz",
                Email = "xyz@gmail.com",
                icon = "Green.jpeg",
                Active = true

            }); customerInfo.Add(new CustomerInfo
            {
                Username = "opq",
                Email = "opq@gmail.com",
                icon = "Green.jpeg",
                Active = true

            });
            customerInfo.Add(new CustomerInfo
            {
                Username = "abcd",
                Email = "abc@gmail.com",
                icon = "Red1.jpeg",
                Active = false
            }); customerInfo.Add(new CustomerInfo
            {
                Username = "xyz",
                Email = "xyz@gmail.com",
                icon = "Green.jpeg",
                Active = true

            }); customerInfo.Add(new CustomerInfo
            {
                Username = "opq",
                Email = "opq@gmail.com",
                icon = "Green.jpeg",
                Active = true

            });
            report.Add(new Report
            {
                UserName = "Pankhaniya Parthik",
                Date = "16/10/2022",
                TtlOrder = 40,
            });
            report.Add(new Report
            {
                UserName = "Madhav Suba",
                Date = "28/02/2022",
                TtlOrder = 70,
            });
            report.Add(new Report
            {
                UserName = "Mansuri lookman",
                Date = "20/07/2022",
                TtlOrder = 130,
            });

            


            ProcutListModel.Add(new ProductListModel
            {
                Title = AppResources.ProcutTitle,
                Brand = AppResources.ProductBrand,
                Id = 1,
                Image = "Block.png",
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
                Image = "Block.png",
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
                Image = "Block.png",
                Price = 299,
                VisibleItemDelete = false,
                ProductList = new string[] { "py_1", "shoesyellow" },
                OldPrice = 400,
                orgId = 2,
                Quantity = 1,
                orderId = 1

            });

            products.Add(new Products
            {
                Stutus = "Best Deals",
                all = "view all",
                productListModel = ProcutListModel.ToList()
            });
            products.Add(new Products
            {
                Stutus = "Top Deals",
                productListModel = ProcutListModel.ToList()
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
        public static async Task<List<ProductListModel>> GetMostSellerProductsByOrganizations(int orgId, int? UserId = null)
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
        public static async Task<List<ProductListModel>> GetAllProductsByOrganizations(int orgId,int? UserId = null)
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
                    AppSettings.ApiUrl + "api/Products/GetAllProductsByOrganizations?org_Id=" + orgId+"&UserID="+ UserId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<List<ProductListModel>> GetAllProductsByCategory(int orgId,int categoryId)
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
                    AppSettings.ApiUrl + "api/Products/GetAllProductsByCategory?orgId=" + orgId+"&CategoryId="+categoryId);

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
                    AppSettings.ApiUrl + "api/Cart/GetAllCartDetails?OrgId=" + orgId + "&UserId=" + userId);

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
        public static async Task<List<ChangeUserData>>GetUserById(int userId, int orgId)
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<List<ChangeAddress>> GetAddressByUserId(int orgId, int userId)
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
        public static async Task<string> UpdateFireBaseToken(FirebaseToken token)
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

                var payload = JsonConvert.SerializeObject(token);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/Notifications/SetUserFirebaseToken", c);

                string result = await response.Content.ReadAsStringAsync();
                return result;
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
                    AppSettings.ApiUrl + "api/Cart/GetOrderDetails?OrgId=" + orgId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<VendorsOrder>>(result);
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
                    AppSettings.ApiUrl + "api/Cart/GetOrderDetailsByOrderMasterId?OrgId=" +orgId + "&OrderMasterId=" +OrderMasterId);
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderDetails>(result);
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
                    AppSettings.ApiUrl +"api/Cart/RemoveFromCart?userId="+userId + "&proId=" +proId + "&orgId=" +orgId);
                string result = await response.Content.ReadAsStringAsync();
                return 0;
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
                return result ;
            }
            catch (Exception ex)

            {
                throw;
            }
        }
        public static async Task<string> WriteToFile(string Message)
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
                    AppSettings.ApiUrl + "api/Logger/WriteToFile?Message=" + Message);

                await response.Content.ReadAsStringAsync();
                return "Success";
            }
            catch (Exception ex)

            {
                throw;
            }
        }
    }
}

