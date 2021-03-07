const express = require("express")

var router = express.Router()

router.get("/", (req, res) => {
    var date = new Date().toISOString().replace("-", "/").split("T")[0].replace("-", "/") // SHEEEEEEEEEEESH!!!!!!!!!!!!!!!!!!!!!!!!!!

    res.render("home", { "date": date })
})

module.exports = router