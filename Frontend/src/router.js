import VueRouter from "vue-router";
import CalendarView from "./views/calendar/CalendarView";
import SearchView from "./views/search/SearchView"

const routes = [
    {
        name: "Calendar",
        path: "/calendar",
        component: CalendarView,
        props: (route) => {
            let splitDate = route.query.date.split("-");
            return {
                date: new Date(splitDate[0], splitDate[1] - 1),
                from: route.query.from.replace("+", " "),
                to: route.query.to.replace("+", " ")
            }
        }
    },
    { name: "Search", path: "/", component: SearchView }
];
const router = new VueRouter({
    mode: "history",
    routes
});

export default router;