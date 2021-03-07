const express = require("express")
const exphbs = require("express-handlebars")
const layouts = require("handlebars-layouts")
const path = require("path")
const hbh = require("../../hbh")

var app = express()
var hbs = exphbs.create({ helpers: hbh })

hbs.handlebars.registerHelper(layouts(hbs.handlebars))
hbs.handlebars.registerPartial("partials/layout", "{{prefix}}")

// Expose some non-sensitive environment variables to view engine
app.locals.env = {
    NAME: process.env.NAME,
    PROPER_NAME: hbh.titlecase(process.env.NAME)
}

app.engine("handlebars", hbs.engine)
app.set("view engine", "handlebars")
app.set("views", path.join(__dirname, "views"))

app.disable("x-powered-by")

app.use("/", require("./routes/main"))

app.use("/static", express.static(path.join(__dirname, "public")))

module.exports.app = app