const vhost = require("vhost")
const express = require("express")
const manifest = require("./servers/manifest.json")
const epiculy = "SPECIAL_ITEM_PLEASE_IGNORE_ME_DONT_ADD_THIS_TO_HOSTS_FILE_ADD_THE_ONES_BELOW_INSTEAD"

global.rboxlo = {}

let app = express()
let hosting = []

if (!process.env.DOCKER) {
    if (process.env.SERVER_OVERRIDE_DOCKER.toLowerCase() == "false") {
        console.log("Not running in Docker, exiting...")
        process.exit(1)
    }

    // Person isn't running in Docker and they're overriding the check.
    // They must be debugging. If they aren't, they're stupid.
    // Will consume all host environment variables into global.rboxlo.env.

    require("dotenv").config({
        path: require("path").join(__dirname, "..", ".env")
    })
}

// Set environment variables to a table named global.rboxlo.env
// These are separate from process.env as they are parsed for booleans, and are cached; accessing process.env directly blocks for each call
global.rboxlo.env = {}
let keys = Object.keys(process.env)

for (let i = 0; i < keys.length; i++) {
    let value = process.env[keys[i]]
    
    if (value.toLowerCase() == "false") {
        value = false
    } else if (value.toLowerCase() == "true") {
        value = true
    }
    
    global.rboxlo.env[keys[i]] = value
}

// Set Node debugging variables
process.env.NODE_ENV = (global.rboxlo.env.PRODUCTION ? "production" : "development")
if (!global.rboxlo.env.PRODUCTION) process.env.DEBUG = "express:*"

// Load all the services (O(n^2))
for (const [name, service] of Object.entries(manifest)) {
    for (let i = 0; i < service.domains.length; i++) {
        let domain = (service.domains[i] == epiculy ? "" : service.domains[i].toLowerCase())

        if (hosting.includes(domain)) {
            throw `Duplicate vhost was found for service "${name}", vhost was "${domain}"`
        }

        hosting.push(domain)
        app.use(vhost(`${domain}${domain == "" ? "" : "."}${global.rboxlo.env.SERVER_DOMAIN}`, require(`.${service.entrypoint}`).app))
    }
}

// Start the server
app.listen(global.rboxlo.env.SERVER_PORT, () => {
    console.log(`Running ${global.rboxlo.env.NAME} on port ${global.rboxlo.env.SERVER_PORT}`)
})

// Okra will be loaded somewhere here...