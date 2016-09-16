$(document).ready(function () {
    console.log("document loaded");
    $("#Client_Id").val($("#IdClient").val());
    $("#Product_Id").val($("#IdProduct").val());
    $.ajax({
        method: "GET",
        url: "http://localhost:59579/api/ProductsAPI/GetProduct/",
        data: { id: $("#Product_Id").val() }
    })
          .done(function (msg) {
              //console.log(msg);
              $("#Product_Price").attr('value', msg["Price"]);

              console.log($("#Product_Price").val() * $("#Number").val());
              $("#labelCalc").text($("#Product_Price").val() * $("#Number").val());
          });
});

$(function () {
    $("#BuyAt").datepicker();
});

$(function () {
    $("#IdClient").on("change", function () {
        $("#Client_Id").val($(this).val());
    });
});

$(function () {
    $("#IdProduct").on("change", function () {
        $("#Product_Id").val($(this).val());
    });
});

$(function () {
    $("#IdProduct").on("change", function () {
        $.ajax({
            method: "GET",
            url: "http://localhost:59579/api/ProductsAPI/GetProduct/",
            data: { id: $("#Product_Id").val() }
        })
          .done(function (msg) {
              //console.log(msg);
              $("#Product_Price").attr('value', msg["Price"]);
              console.log($("#Product_Price").val() * $("#Number").val());
              $("#labelCalc").text($("#Product_Price").val() * $("#Number").val());
          });
    });
});

$(function () {
    $("#Number").on("change", function () {
        console.log($("#Product_Price").val() * $("#Number").val());
        $("#labelCalc").text($("#Product_Price").val() * $("#Number").val());
    });
}
);