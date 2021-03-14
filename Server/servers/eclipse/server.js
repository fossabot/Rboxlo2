const express = require("express")
const exphbs = require("express-handlebars")
const layouts = require("handlebars-layouts")
const path = require("path")
const hbh = require("../../hbh")
const util = require("../../util")

let app = express()

// Static resources (CSS, JavaScript, images, etc.)
app.use(express.static(path.join(__dirname, "public")))

// Expose some non-sensitive environment variables to the view engine
app.locals.env = {
    NAME: global.rboxlo.env.NAME,
    PROPER_NAME: util.titlecase(global.rboxlo.env.NAME),
    VERSION: util.getVersion(),
    DOMAIN: global.rboxlo.env.SERVER_DOMAIN,
    PROPER_DOMAIN: `${global.rboxlo.env.SERVER_HTTPS ? "https://" : "http://"}${global.rboxlo.env.SERVER_DOMAIN}`,
    DSR: (global.rboxlo.env.PRODUCTION ? ".min" : "") // "Debug Static Resource"
}

// Set up view engine
let hbs = exphbs.create({ helpers: hbh })

hbs.handlebars.registerHelper(layouts(hbs.handlebars))
hbs.handlebars.registerPartial("partials/layout", "{{prefix}}")

app.engine("handlebars", hbs.engine)
app.set("view engine", "handlebars")
app.set("views", path.join(__dirname, "views"))

// X-Powered-By header
// A: Why is it an ASCII char array? To hopefully deter CTRL+SHIFT+Fs of "Rboxlo"
// B: Why is "Rboxlo" hardcoded here? Because Rboxlo is the application powering it
app.disable("x-powered-by")
if (global.rboxlo.env.SERVER_X_POWERED_BY) {
    app.use((req, res, next) => {
        let poweredBy = [ 82, 98, 111, 120, 108, 111, 47, 49, 46, 48, 46, 48 ] // "Rboxlo/1.0.0"
        res.setHeader("X-Powered-By", String.fromCharCode.apply(null, poweredBy))

        next()
    })
}

// Routes
app.use("/", require("./routes/main"))

module.exports.app = app