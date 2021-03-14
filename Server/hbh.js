// handlebars turing complete when??

var exports = module.exports = {}

const util = require("./util")

exports.titlecase = util.titlecase

exports.strcmp = (x, y, opt) => {
    return (x == y) ? opt.fn(this) : opt.inverse(this)
}

exports.strcicmp = (x, y, opt) => {
    return (x.toLowerCase() == y.toLowerCase()) ? opt.fn(this) : opt.inverse(this)
}