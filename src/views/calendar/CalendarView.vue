<template>
  <div class="calendar">
    <div v-for="row in 5" :key="row" class="columns">
      <div
        v-for="column in 7"
        :key="column"
        class="column"
      >
        <CalendarColumn>
          <template #date>
            {{}}
          </template>
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
      required: true
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
        date = new Date(date.getTime() + (1000 * 60 * 60 * 24));
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
  margin: 0.5rem;
}
.calendar {
  margin: 5em 0 5em 0;
  max-width: 90%;
}
</style>