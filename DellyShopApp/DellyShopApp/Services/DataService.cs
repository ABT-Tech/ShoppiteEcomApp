using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Views.Pages;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
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
        public List<Attributes> attributes = new List<Attributes>();
        public ChangeUserData EditProfile = new ChangeUserData();
        public List<ChangeAddress> changeAddress = new List<ChangeAddress>();
        public List<CategoryDetailPage> Details = new List<CategoryDetailPage>();
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
            Carousel.Add(new Category
            {
                Banner = "shoes.jpg",
                CategoryId = "1"
            });
            Carousel.Add(new Category
            {
                Banner = "elecronics.jpeg",
                CategoryId = "3"
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
        public static async Task<List<OrgCategories>> GetAllOrganizationCategories()
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
                    AppSettings.ApiUrl + "api/Organization/GetAllOrganizationCategories");

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<OrgCategories>>(result);
            }
            catch (Exception ex)
            {
                throw;

            }
        }
        public static async Task<List<ShopModel>> GetAllOrganizations( int Org_CategoryId)
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
                    AppSettings.ApiUrl + "api/Organization/GetAllOrganizations?Org_CategoryId="+ Org_CategoryId);

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
        public static async Task<ProductMasterResponse> GetAllProductsByOrganizations(int orgId,int? UserId = null, int? OrgCatId = 0)
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
                    AppSettings.ApiUrl + "api/Products/GetAllProductsByOrganizations?org_Id=" + orgId+"&UserID="+ UserId + "&orgcat_id=" + OrgCatId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductMasterResponse>(result);
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
        public static async Task<CouponMsg> DisCoupon(DiscountCoupon discountCoupon)
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
                var payload = JsonConvert.SerializeObject(discountCoupon);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.BaseAddress = new Uri(AppSettings.ApiUrl);
                var response = await httpClient.PostAsync("api/User/ApplyCoupon", c);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CouponMsg>(result); 
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
        public static async Task<int> RemoveFromCart(int userId, int orgId, int proId,int SpecificationId)
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
                    AppSettings.ApiUrl +"api/Cart/RemoveFromCart?userId="+userId + "&proId=" +proId + "&orgId=" +orgId+ "&SpecificationId=" +SpecificationId);
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
        public static async Task<OrderCheckOut> GetOnePlayFlag(int orgId)
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
                    AppSettings.ApiUrl + "api/Cart/GetOnePlayFlag?OrgId=" + orgId );

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderCheckOut>(result); 
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<PaymentGatewayResponse> MakePaymentRequest(OrderCheckOut orderCheckOut)
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
                var response = await httpClient.PostAsync("api/Cart/MakePaymentRequest", c);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaymentGatewayResponse>(result);
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
        public static async Task<List<ProductListModel>> GetSimilarProducts(int orgId, int categoryId, int BrandId)
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
                    AppSettings.ApiUrl + "api/Products/GetSimilarProducts?OrgId=" + orgId + "&CategoryId=" + categoryId + "&BrandId=" + BrandId);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductListModel>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<List<Attributes>> GetProductVariation(int OrgId,Guid ProductGUID)
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
                    AppSettings.ApiUrl + "api/Products/GetProductVariation?OrgId=" + OrgId + "&ProductGUId=" + ProductGUID);

                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Attributes>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<ProductListModel> GetProductDetailsBySpecifcation(int OrgId, Guid ProductGUID ,int SpecificationId, int UserId)
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
                var userData = UserId.ToString() == "0" ? "" : UserId.ToString();
                var response = await httpClient.GetAsync(
                    AppSettings.ApiUrl + "api/Products/GetProductDetailsBySpecifcation?OrgId="+ OrgId +"&ProductGUID=" + ProductGUID + "&SpecificationId=" + SpecificationId+ "&UserId=" + userData);
                    

                string result = await response.Content.ReadAsStringAsync();
                 var STORE = JsonConvert.DeserializeObject<List<ProductListModel>>(result);
                ProductListModel product = new ProductListModel();
                
                foreach (var Single in STORE)
                {
                    product = Single;
                }

                return product; //JsonConvert.DeserializeObject<ProductListModel>(STORE);
            }

            catch (Exception ex)
            {
                throw;
            }
        }
        public string EncryptPaymentRequest(string merchantId, string key, string merchantParamsJson)
        {
            String encryptedText = string.Empty;
            try
            {
                string original = merchantParamsJson;
                string merchantEncryptionKey = key.Substring(0, 16);
                encryptedText = EncryptAES256_V3(merchantParamsJson, key, merchantEncryptionKey);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return encryptedText;
        }
        public string DecryptAES256_V3(string cipherText, string _key, string _iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            byte[] key = Encoding.ASCII.GetBytes(_key);
            byte[] iv = Encoding.ASCII.GetBytes(_iv);

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            encryptor.Key = key;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            string plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }

            // Return the decrypted data as a string
            return plainText;
        }

        public string EncryptAES256_V3(string plainText, string _key, string _iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();
            byte[] key = Encoding.ASCII.GetBytes(_key);
            byte[] iv = Encoding.ASCII.GetBytes(_iv);
            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            encryptor.Key = key;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            // Convert the plainText string into a byte array
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            // Encrypt the input plaintext string
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            // Complete the encryption process
            cryptoStream.FlushFinalBlock();

            // Convert the encrypted data from a MemoryStream to a byte array
            byte[] cipherBytes = memoryStream.ToArray();

            // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();

            // Convert the encrypted byte array to a base64 encoded string
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            // Return the encrypted data as a string
            return cipherText;
        }
       
    }
}

