const http = require("http");
const fs = require("fs");
const express = require("express");
const cors = require("cors");
const path = require("path");

const app = express();
const licenseList = [
  {
    license: { regNumber: 1435 - 252, licenceId: 21522, isConnected: true },
    user: { name: "Yasser", address: "Riyadh - ksa" }
  },
  {
    license: { regNumber: 1435 - 300, licenceId: 33232, isConnected: false },
    user: { name: "Ahmed", address: "Jeddah - ksa" }
  }
];
app.use(
  cors({
    origin: "http://localhost:4000"
  })
);

app.get("/getDashboard", cors(), (req, resp) => {
  resp.send(licenseList);
});

app.listen(3100, () => {
  console.log("listining on port 3100");
});
