<template>
  <div id="calendar">
    <b-loading :active.sync="isLoading"></b-loading>
    <h1>Showing prices from {{this.from}} to {{this.to}}</h1>
    <h2>{{this.months[this.date.getMonth()]}} - {{this.date.getFullYear()}}</h2>
    <div class="columns calendar__columns">
      <div v-for="day in dayNames" :key="day" class="column" style="margin-left: 2%;">
        <span class="has-text-weight-bold">{{day}}</span>
      </div>
    </div>
  </div>
</template>
<script>
import CalendarCell from "./CalendarCell";
import Vue from "vue";
import api from "../../priceApi";

export default {
  props: {
    date: {
      type: Date,
      default: () => new Date()
    },
    to: {
      type: String,
      default: () => ""
    },
    from: {
      type: String,
      default: () => ""
    }
  },
  methods: {
    getDates: function(date) {
      this.firstDay = new Date(date.getFullYear(), date.getMonth()).getDay();
      this.firstDay = this.firstDay ? this.firstDay - 1 : 6;
      this.daysInMonth =
        32 - new Date(date.getFullYear(), date.getMonth(), 32).getDate();
    },
    populateCalendar: function(priceArray) {
      var CalendarCellClass = Vue.extend(CalendarCell);

      let today = new Date();

      let lowestPrice = Math.min(...priceArray.filter(price => price !== null && price.amount != 0)
      .map(price => price.amount));

      let calendarContainer = document.getElementById("calendar");
      let priceCounter = 0;
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
            if (j % 7 !== 0) {
              let divWrapper = document.createElement("div");
              divWrapper.classList.add("column", "calendar__columns__column");
              row.appendChild(divWrapper);
            } else {
              break;
            }
            // Add an actual cell with date (and price if later than today)
          } else {
            let cell = new CalendarCellClass();
            cell.$slots.date = date;
            // Prices are for some month in the future
            if (
              new Date(this.date.getFullYear(), this.date.getMonth(), 1) > today
            ) {
              let price = priceArray[priceCounter++];
              cell.$slots.price = price !== null ? price.amount : null;
              if (price !== null) {
                cell.$props.nightTrain = price.nightTrain;
              }
            }
            // Prices are for this month
            else if (date >= today.getDate()) {
              let price = priceArray[priceCounter++]; 
              cell.$slots.price = price !== null ? price.amount : null;
              if (price !== null) {
                cell.$props.nightTrain = price.nightTrain;
              }
            }

            let domCell = cell.$mount();
            if (cell.$slots.price === lowestPrice) {
              domCell.$el.classList.add("cheapest");
            }

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
    api.getPrices(this.date, this.to, this.from).then(response => {
      this.prices = response.data;
      this.populateCalendar(response.data);
      this.isLoading = false;
    });
  },
  data() {
    return {
      isLoading: true,
      firstDay: undefined,
      daysInMonth: undefined,
      dayNames: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday"
      ],
      months: [
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
      ],
      prices: undefined
    };
  }
};
</script>
<style lang="scss">
@import "../../assets/style/variables.scss";

#calendar {
  margin: 3em 0 5em 0;
}
.calendar__columns__column {
  padding: 0;
  margin: 0.2rem;
  height: 5rem;
}
h1 {
  color: $black-bis;
  font: $family-sans-serif;
  font-size: $size-1;
}
h2 {
  color: $grey-dark;
  font: $family-sans-serif;
  font-size: $size-3;
  margin-bottom: 1em;
}
</style>