const axios = require("axios").default;

const instance = axios.create({
    baseURL: "http://localhost:5000",
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