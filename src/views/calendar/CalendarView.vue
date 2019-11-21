<template>
  <div class="calendar">
    <div class="columns">
      <div v-for="day in dayNames" :key="day" class="column">{{day}}</div>
    </div>
    <div v-for="index in 5" :key="index" class="columns">
      <div v-for="index in 7" :key="index" class="column">
        <CalendarColumn>
          <template #date>{{}}</template>
        </CalendarColumn>
      </div>
    </div>
  </div>
</template>
<script>
import CalendarColumn from "./CalendarColumn";

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
    // https://medium.com/@emamoah/building-a-calendar-from-scratch-with-vue-js-and-css-grid-no-libraries-dec53062ee25
    getDates: function(date) {
      this.dates = [];

      date.setDate(1);
      let month = date.getMonth();
      while (date.getMonth() === month) {
        this.dates.push({ date: date.getDate(), day: date.getDay() });
        date = new Date(date.getTime() + 1000 * 60 * 60 * 24);
      }
      this.firstSundayDate = 8 - this.dates[0].day;
    }
  },
  created: function() {
    this.getDates(this.date);
  },
  data() {
    return {
      firstSundayDate: undefined,
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
.calendar {
  margin: 5em 0 5em 0;
}
</style>