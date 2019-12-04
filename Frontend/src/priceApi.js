const axios = require("axios").default;

const instance = axios.create({
    baseURL: "https://localhost:5001",
    timeout: 30000
})

function getPrices(date) {
    return instance.get("/journey", {
        params: {
            date: date
        }
    });
}

export default {
    getPrices
}