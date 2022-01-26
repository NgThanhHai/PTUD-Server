using DotnetServer.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DotnetServer.Services
{


    public class OrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly ShopService _shopService;
        private readonly CustomerService _customerService;
        public OrderService(IDatabaseSettings settings, ShopService ShopService, CustomerService CustomerService)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _orderCollection = mongoDatabase.GetCollection<Order>("Order");
            _shopService = ShopService;
            _customerService = CustomerService;
        }

        public List<OrderResponse> Get(orderBodyRequest request)
        {
            var result = new List<OrderResponse>();
            var dummy = _orderCollection.Find(x =>

             (request.shop_id == null || x.shop_id == request.shop_id)
             && (request.shipper_id == null || x.shipper_id == request.shipper_id)
             && (request.status == null || x.status == request.status)
             && ((request.from == null && request.to == null)
                    || (request.from == null && request.to != null && x.created_at <= request.to)
                    || (request.from != null && request.to == null && x.created_at >= request.from)
                    || (request.from != null && request.to != null && request.from >= x.created_at && x.created_at <= request.to)
             )
             ).ToList().OrderByDescending(x => x.created_at).ToList();

            for (int i = 0; i < dummy.Count; i++)
            {
                var cus = _customerService.Get(dummy[i].customer_id);
                var shop = _shopService.Get(dummy[i].shop_id);
                if (cus == null || shop == null)
                {
                    continue;
                }

                var curValue = dummy[i];
                var resultDummy = new OrderResponse();
                resultDummy._id = curValue._id;
                resultDummy.shop_id = curValue.shop_id;
                resultDummy.customer_id = curValue.customer_id;
                resultDummy.shipper_id = curValue.shipper_id;
                resultDummy.status = curValue.status;
                resultDummy.total = curValue.total;
                resultDummy.shipper_fee = curValue.shipper_fee;
                resultDummy.cert_shop = curValue.cert_shop;
                resultDummy.cert_cus = curValue.cert_cus;
                resultDummy.created_at = curValue.created_at;
                resultDummy.updated_at = curValue.updated_at;
                resultDummy.ship_info = curValue.ship_info;
                resultDummy.shop_name = shop.name;
                resultDummy.cus_name = cus.name;
                resultDummy.order_detail = curValue.order_detail;
                result.Add(resultDummy);
            }

            return result;
        }




        public OrderResponse Get(string id) {
            var curValue = _orderCollection.Find(x => x._id == id).FirstOrDefault();

            var cus = _customerService.Get(curValue.customer_id);
            var shop = _shopService.Get(curValue.shop_id);
            var resultDummy = new OrderResponse();

            if (cus == null || shop == null)
            {
                return resultDummy;
            }

            resultDummy._id = curValue._id;
            resultDummy.shop_id = curValue.shop_id;
            resultDummy.customer_id = curValue.customer_id;
            resultDummy.shipper_id = curValue.shipper_id;
            resultDummy.status = curValue.status;
            resultDummy.total = curValue.total;
            resultDummy.shipper_fee = curValue.shipper_fee;
            resultDummy.cert_shop = curValue.cert_shop;
            resultDummy.cert_cus = curValue.cert_cus;
            resultDummy.created_at = curValue.created_at;
            resultDummy.updated_at = curValue.updated_at;
            resultDummy.ship_info = curValue.ship_info;
            resultDummy.shop_name = shop.name;
            resultDummy.cus_name = cus.name;
            resultDummy.order_detail = curValue.order_detail;
            return resultDummy;
        }
        public Order GetOrigin(string id) => _orderCollection.Find(x => x._id == id).FirstOrDefault();
        public Order Create(Order newOrder)
        {
            var dummy = newOrder;
             dummy.created_at = DateTime.Now;
            dummy.updated_at = DateTime.Now;
            _orderCollection.InsertOne(dummy);
            return newOrder;
        }

        public Order Update(string id, Order updatedOrder)
        {
            _orderCollection.ReplaceOne(x => x._id == id, updatedOrder);
            return updatedOrder;
        }

        
        public void Remove(string id) =>
             _orderCollection.DeleteOne(x => x._id == id);

        public async Task<string> SendMail(string _from, Customer _to, string _subject, string _body, SmtpClient client)
        {
            // Tạo nội dung Email
            MailMessage message = new MailMessage(
                from: _from,
                to: _to.identify,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);


            try
            {
                await client.SendMailAsync(message);
                var cus = _customerService.Get(_to._id);
                if (cus == null) {
                    return null;
                }

                var newCus = cus;
                newCus.otp = _body;
                _customerService.Update(_to._id, cus);
                return _to._id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> SendMailGoogleSmtp(string phone)
        {

            var body = Generate_otp(6);
            var username = "trungnemn@gmail.com";
            var pass = "trung30032000";
            var _to = _customerService.GetCusFromPhone(phone);
            if (_to == null)
            {
                return null;
            }
            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.Credentials = new NetworkCredential(username, pass);
                client.EnableSsl = true;
                return await SendMail(username, _to, "Send OTP PTUD", body, client);
            }

        }

        public string Generate_otp(int size)
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < size; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }

        public bool VerifyOTP(string id, string otp)
        {
            var cus = _customerService.Get(id);
            if (cus == null)
            {
                return false;
            }
            
            if (cus.otp != otp)
            {
                return false;
            }

            var newCus = cus;
            newCus.otp = "";
            _customerService.Update(id, newCus);

            return true;


        }
    }

}
   
