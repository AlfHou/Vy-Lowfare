import VueRouter from "vue-router";
import CalendarView from "./views/calendar/CalendarView";
import SearchView from "./views/search/SearchView"

const routes = [
    {
        name: "Calendar",
        path: "/calendar",
        component: CalendarView,
        props: true 
    },
    { path: "/", component: SearchView }
];
const router = new VueRouter({
    mode: "history",
    routes
});

export default router;