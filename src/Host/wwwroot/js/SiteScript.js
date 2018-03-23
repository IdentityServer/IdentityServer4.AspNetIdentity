var qrObj = new QRCode(document.getElementById("qrcode"), {
    width: 150,
    height: 150
});
var srText = document.getElementById("qrCodeData").getAttribute("data-url")

qrObj.makeCode(srText);
