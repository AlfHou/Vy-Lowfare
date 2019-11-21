<template>
  <div class="calendar">
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
      date.setDate(1);
      let month = date.getMonth();
      while (date.getMonth() === month) {
        date = new Date(date.getTime() + 1000 * 60 * 60 * 24);
      }
    }
  },
  created: function() {
    this.getDates(this.date);
  }
};
</script>
<style scoped>
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