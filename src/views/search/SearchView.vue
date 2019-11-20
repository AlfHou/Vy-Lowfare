<template>
  <section class="card">
    <div class="card-content columns is-centered">
      <div class="column">
        <Field @destChange="updateFrom" label="From" placeholder="Where From?" />
        
      </div>
      <div class="column">
        <Field @destChange="updateTo" label="To" placeholder="Where to?" />
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
//import EnturService, {convertFeatureToLocation} from "@entur/sdk";
import EnturService from "@entur/sdk";

const entur = new EnturService({ clientName: "AlfHouge-LowfareTrain" });

export default {
  components: {
    Field
  },
  methods: {
     search: function() {
       this.$router.push({name: "Calendar"}) 
     },
    //search: function() {
    //  let from = convertFeatureToLocation(this.from);
    //  let to = convertFeatureToLocation(this.to);
    //  let query = {
    //    searchDate: this.departure,
    //    from: from,
    //    to: to,
    //    limit: 20,
    //    modes: ["rail"]
    //  };
    //  console.log("Query: ", query)
    //  /*eslint no-console: "off"*/
    //  entur.getTripPatterns(query).then(res => {
    //    console.log(res);
    //  });
    //},
    updateFrom: function(dest) {
      // Get first result
      entur.getFeatures(dest).then(result => {
        this.from = result[0];
      });
    },
    updateTo: function(dest) {
      entur.getFeatures(dest).then(result => {
        this.to = result[0];
      });
    }
  },
  data() {
    return {
      from: undefined,
      to: undefined,
      departure: new Date()
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