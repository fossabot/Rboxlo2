var exports = module.exports = {}

const fs = require("fs")
const path = require("path")

exports.titlecase = (text) => {
    text = text.toLowerCase().split(" ")
    for (var i = 0; i < text.length; i++) {
        text[i] = text[i].charAt(0).toUpperCase() + text[i].slice(1)
    }

    return text.join(" ")
}

exports.readFile = (path) => {
    return fs.readFileSync(path, { encoding: "utf8", flag: "r" })
}

exports.getVersion = () => {
    let version = `${exports.titlecase(global.rboxlo.env.NAME)} - ${exports.readFile(path.join(__dirname, "packaging", "version"))}`

    if (fs.existsSync(path.join(__dirname, "packaging", "commit"))) {
        version += ` (${exports.readFile(path.join(__dirname, "packaging", "commit")).replace(/^\s+|\s+$/g, "").substring(0, 7)})`
    }

    return version
}

exports.getVersion()