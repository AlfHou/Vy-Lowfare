const axios = require("axios").default;

const instance = axios.create({
    baseURL: "https://itinerary.cloud.nsb.no",
    timeout: 45000
})

function getStops() {
    return instance.get("/api/stops");
}
export default {
    getStops
}