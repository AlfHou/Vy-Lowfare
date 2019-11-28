<template>
  <div id="calendar">
    <!-- <div class="columns">
      <div v-for="day in dayNames" :key="day" class="column">
        <span class="has-text-weight-bold">{{day}}</span>
      </div>
    </div>
    <div v-for="row_index in 5" :key="row_index" class="columns">
      <div v-for="column_index in 7" :key="column_index" class="column">
        <CalendarColumn>
          <template #date>{{"test"}}</template>
        </CalendarColumn>
      </div>
    </div>-->
  </div>
</template>
<script>
/*eslint no-console: "off"*/
/*eslint no-unused-vars: "off"*/
/*eslint vue/no-unused-components: "off"*/

import CalendarColumn from "./CalendarColumn";
import Vue from "vue";

export default {
  props: {
    date: {
      type: Date,
      default: () => new Date()
    }
  },
  components: {
    CalendarColumn
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
        row.className = "columns";

        calendarContainer.appendChild(row);

        // For each individual day
         for (let j = 0; j < 7; j++) {
          if (i === 0 && j < this.firstDay) {
            let cell = new CalendarColumnClass();
            var domCell = cell.$mount()
            row.appendChild(domCell.$el);
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
<style scoped lang="scss">
@import "../../assets/style/variables.scss";

.header {
  background-color: $dark;
  margin-bottom: 2%;
  height: 18%;
  border-radius: $radius;
}
.column {
  padding: 0;
}
.columns {
  height: 26%;
}
#calendar {
  margin: 5em 0 5em 0;
}
</style>