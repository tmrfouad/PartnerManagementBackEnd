using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order {
    public bool delivery_needed { get; set; }
    public int merchant_id { get; set; }      // merchant_id obtained from step 1
    public float amount_cents { get; set; }
    public string currency { get; set; }
    [Key]
    public int merchant_order_id { get; set; }  // unique alpha-numerice value; example: E6RR3
    [NotMapped]
    public object[] items { get; set; }
    public ShippingData shipping_data { get; set; }
    public int shipping_dataId { get; set; }
    public bool MailSent { get; set; } = false;
}