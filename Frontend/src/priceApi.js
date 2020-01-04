const axios = require("axios").default;

const API_ENDPOINT = process.env.NODE_ENV === "development" ?
    "http://localhost:5000/api" : "http://lowfare-train.alfhouge.no/api";

const instance = axios.create({
    baseURL: API_ENDPOINT,
    timeout: 45000
})

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