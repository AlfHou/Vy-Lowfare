<template>
  <section class="card">
    <div class="card-content columns is-centered">
      <div class="column">
        <Field @destChange="updateFrom" label="From" placeholder="Where From?" :stops="stops" />
      </div>
      <div class="column">
        <Field @destChange="updateTo" label="To" placeholder="Where to?" :stops="stops" />
      </div>
    </div>
    <div class="columns is-centered">
      <div class="column is-half">
        <b-field class="date-picker" label="Departure">
          <b-datepicker
            v-model="departure"
            placeholder="Type or select a date..."
            icon="calendar-today"
            editable
            type="month"
            size="is-medium"
          ></b-datepicker>
        </b-field>
      </div>
    </div>
    <div class="columns is-centered">
      <div class="column is-one-quarter search-column">
        <b-button type="is-primary" size="is-large" expanded @click="search">Search</b-button>
      </div>
    </div>
  </section>
</template>
<script>
import Field from "./Field";
import api from "../../vyApi";

export default {
  components: {
    Field
  },
  created: function() {
    api.getStops().then(response => {
      this.stops = response.data
        .filter(stop => stop.type == "TRAIN")
        .map(stop => stop.name);
    });
  },

  methods: {
    search: function() {
      if (this.to == this.from) {
        this.$buefy.notification.open({
          duration: 3000,
          message: "To and from can not be the same",
          position: "is-bottom",
          type: "is-danger",
          hasIcon: false
        });
        return;
      }
      let today = new Date();
      if (
        new Date(this.departure.getFullYear(), this.departure.getMonth(), 1) <
        new Date(today.getFullYear(), today.getMonth(), 1)
      ) {
        this.$buefy.notification.open({
          duration: 5000,
          message:
            "Selected month is earlier than this month. " +
            "Please select current month, or a month in the future",
          position: "is-bottom",
          type: "is-danger",
          hasIcon: false
        });
        return;
      }

      this.$router.push({
        name: "Calendar",
        query: {
          date:
            this.departure.getFullYear() +
            "-" +
            (this.departure.getMonth() + 1),

          to: this.to.replace(/ /g, "+"),
          from: this.from.replace(/ /g, "+")
        }
      });
    },
    updateFrom: function(dest) {
      this.from = dest;
    },
    updateTo: function(dest) {
      this.to = dest;
    }
  },
  data() {
    return {
      from: undefined,
      to: undefined,
      departure: new Date(),
      stops: undefined,
      test: ["Oslo S", "Trondheim"]
    };
  }
};
</script>
<style scoped lang="scss">
@import "../../assets/style/variables.scss";
.search-column {
  text-align: center;
  margin-bottom: -2.5rem;
}
.date-picker {
  border-bottom: 1px solid $primary;
  border-radius: $radius;
  margin-bottom: 3rem;
}
</style>