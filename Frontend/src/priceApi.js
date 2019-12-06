const axios = require("axios").default;

const instance = axios.create({
    baseURL: "https://localhost:5001",
    timeout: 45000
})

function getPrices(date, to, from) {
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