import { OrderPdfResponse } from "../api/order-service-api";

export function openPdfInNewTab(order: OrderPdfResponse) {
  const decodedData = window.atob(order.pdf!);
  const uInt8Array = new Uint8Array(decodedData.length);
  for (let i = 0; i < decodedData.length; ++i) {
    uInt8Array[i] = decodedData.charCodeAt(i);
  }
  var blob = new Blob([uInt8Array], {
    type: "application/pdf",
  });
  const url = window.URL.createObjectURL(blob);
  const link = document.createElement("a");
  link.href = url;
  link.target = "_blank";
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
    window.open(
      "data:application/pdf," + encodeURI(order.pdf!),
      order.fileName!,
      order.fileName!
    );
}
