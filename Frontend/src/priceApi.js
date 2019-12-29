const axios = require("axios").default;

const API_ENDPOINT = process.env.NODE_ENV  === "development" ? 
    "http://localhost:5000" : "http://lowfare-train.alfhouge.no";

const instance = axios.create({
    baseURL: API_ENDPOINT,
    timeout: 45000
})
// TODO: Have NGINX proxy all traffic from /api to the correct docker port

function getPrices(date, to, from) {
    // Have to adjust to take timezone into account...
    let hoursDiff = date.getHours() - date.getTimezoneOffset() / 60;
    date.setHours(hoursDiff);
    return instance.get("/journey", {
        params: {
            date: date,
            to: to,
            from: from
        }
    });
}

export default {
    getPrices
}