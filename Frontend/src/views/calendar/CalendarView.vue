<template>
  <div id="calendar">
    <div class="columns calendar__columns">
      <div v-for="day in dayNames" :key="day" class="column" style="margin-left: 2%;">
        <span class="has-text-weight-bold">{{day}}</span>
      </div>
    </div>
  </div>
</template>
<script>
import CalendarColumn from "./CalendarColumn";
import Vue from "vue";

export default {
  props: {
    date: {
      type: Date,
      default: () => new Date()
    }
  },
  methods: {
    getDates: function(date) {
      this.firstDay = new Date(date.getFullYear(), date.getMonth()).getDay();
      this.daysInMonth =
        32 - new Date(date.getFullYear(), date.getMonth(), 32).getDate();
    },
    populateCalendar: function() {
      var CalendarColumnClass = Vue.extend(CalendarColumn);

      let calendarContainer = document.getElementById("calendar");
      let date = 1;
      for (let i = 0; i < 6; i++) {
        let row = document.createElement("div");
        row.classList.add("columns", "calendar__columns");

        calendarContainer.appendChild(row);

        // For each individual day
        for (let j = 0; j < 7; j++) {
          // All days that are before the first day in the given month
          if (i === 0 && j < this.firstDay) {
            let divWrapper = document.createElement("div");
            divWrapper.classList.add("column", "calendar__columns__column");
            row.appendChild(divWrapper);
          } else if (date > this.daysInMonth) {
            break;
          } else {
            let cell = new CalendarColumnClass();
            cell.$slots.date = date;
            let domCell = cell.$mount();

            let divWrapper = document.createElement("div");
            divWrapper.classList.add("column", "calendar__columns__column");
            row.appendChild(divWrapper);

            divWrapper.appendChild(domCell.$el);
            date++;
          }
        }
      }
    }
  },
  mounted: function() {
    this.getDates(this.date);
    this.populateCalendar();
  },
  data() {
    return {
      firstDay: undefined,
      daysInMonth: undefined,
      dates: undefined,
      dayNames: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday"
      ]
    };
  }
};
</script>
<style lang="scss">
@import "../../assets/style/variables.scss";

#calendar {
  margin: 5em 0 5em 0;
}
.calendar__columns__column {
  padding: 0;
  margin: 0.2rem;
  height: 5rem;
}
</style>