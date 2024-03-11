using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ShopService : BaseClass
{
    public static string GetProductImage(string prodNo)
    {
        return $"~/images/product/{prodNo}/{prodNo}.jpg";
    }

    public List<string> GetProductImageList(string prodNo)
    {
        string str_prod_file = $"{prodNo}.jpg";
        List<string> ImageList = new List<string>();
        var rootFolder = Directory.GetCurrentDirectory();
        string str_path = $"wwwroot\\images\\product\\{prodNo}";
        string str_dir = Path.Combine(rootFolder, str_path);
        var fileEntries = Directory.GetFiles(str_dir).ToList();
        if (fileEntries.Count() > 0)
        {
            foreach (var item in fileEntries)
            {
                string str_name = Path.GetFileName(item);
                if (str_name != str_prod_file)
                {
                    string data = $"~/images/product/{prodNo}/{str_name}";
                    ImageList.Add(data);
                }
            }
        }
        return ImageList;
    }

    public List<string> GetProductImageListName(string prodNo)
    {
        string str_prod_file = $"{prodNo}.jpg";
        List<string> ImageList = new List<string>();
        var rootFolder = Directory.GetCurrentDirectory();
        string str_path = $"wwwroot\\images\\product\\{prodNo}";
        string str_dir = Path.Combine(rootFolder, str_path);
        var fileEntries = Directory.GetFiles(str_dir).ToList();
        if (fileEntries.Count() > 0)
        {
            foreach (var item in fileEntries)
            {
                string str_name = Path.GetFileName(item);
                if (str_name != str_prod_file)
                {
                    // string data = $"~/images/product/{prodNo}/{str_name}";
                    string data = $"{prodNo}/{str_name}";
                    ImageList.Add(data);
                }
            }
        }
        return ImageList;
    }

    /// <summary>
    /// PhotoNo
    /// </summary>
    /// <value></value>
    public static string PhotoNo { get; set; }


    /// <summary>
    /// 消費者付款
    /// </summary>
    public int CartPayment(vmOrders model)
    {
        int int_order_id = 0;
        // OrderNo = CreateNewOrderNo(model);
        // using (tblCarts carts = new tblCarts())
        // {
        //     using (tblOrders orders = new tblOrders())
        //     {
        //         using (tblOrdersDetail ordersDetail = new tblOrdersDetail())
        //         {
        //             using (tblBooks books = new tblBooks())
        //             {
        //                 var datas = carts.repo.ReadAll(m => m.user_no == SessionService.AccountNo);
        //                 if (datas != null)
        //                 {
        //                     int int_amount = datas.Sum(m => m.amount).GetValueOrDefault();
        //                     decimal dec_tax = 0;
        //                     if (int_amount > 0)
        //                     {
        //                         dec_tax = Math.Round((decimal)(int_amount * 5 / 100), 0);
        //                     }
        //                     int int_total = int_amount + (int)dec_tax;

        //                     var data = orders.repo.ReadSingle(m => m.order_no == OrderNo);
        //                     if (data != null)
        //                     {
        //                         data.amounts = int_amount;
        //                         data.taxs = (int)dec_tax;
        //                         data.totals = int_total;

        //                         orders.repo.Update(data);
        //                         orders.repo.SaveChanges();
        //                     }

        //                     foreach (var item in datas)
        //                     {
        //                         OrdersDetail detail = new OrdersDetail();
        //                         detail.order_no = OrderNo;
        //                         detail.product_no = item.product_no;
        //                         detail.product_name = item.product_name;
        //                         detail.vendor_no = "";
        //                         detail.category_name = books.GetCategoryName(item.product_no);
        //                         detail.product_spec = item.product_spec;
        //                         detail.qty = item.qty;
        //                         detail.price = item.price;
        //                         detail.amount = item.amount;
        //                         detail.remark = "";

        //                         ordersDetail.repo.Create(detail);
        //                         ordersDetail.repo.SaveChanges();
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }
        return int_order_id;
    }
}