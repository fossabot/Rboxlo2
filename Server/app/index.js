const vhost = require("vhost")
const express = require("express")
const manifest = require("./servers/manifest.json")
const epiculy = "SPECIAL_ITEM_PLEASE_IGNORE_ME_DONT_ADD_THIS_TO_HOSTS_FILE_ADD_THE_ONES_BELOW_INSTEAD"

var app = express()

// Huey-loading
// 1. Loops through manifest for servers
// 2. Requires the entrypoint to fetch the Express app
// 3. Loops through domains in server manifest entry
// 4. If special index, then it will make a vhost for this server that is hosted on the domains index
// 4a. If not, then it will create a vhost for this server that is hosted on the subdomain
for (const [name, service] of Object.entries(manifest)) {
    var server = require(`.${service.entrypoint}`).app

    for (var i = 0; i < service.domains.length; i++) {
        if (service.domains[i] == epiculy) {
            app.use(vhost(process.env.SERVER_DOMAIN, server))
        } else {
            app.use(vhost(`${service.domains[i].toLowerCase()}.${process.env.SERVER_DOMAIN}`, server))
        }
    }
}

app.listen(process.env.SERVER_PORT)
console.log(`Running ${process.env.NAME} on port ${process.env.SERVER_PORT}`)