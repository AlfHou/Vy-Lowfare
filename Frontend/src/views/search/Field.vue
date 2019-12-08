<template>
  <b-field :label="label" type="is-dark">
    <b-autocomplete
      v-model="destination"
      :placeholder="placeholder"
      :keep-first="true"
      :open-on-focus="false"
      :data="filteredStops"
      @select="option => selected = option"
      size="is-large"
    ></b-autocomplete>
  </b-field>
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      required: true
    },
    placeholder: {
      type: String,
      required: true
    },
    stops: {
      type: Array,
      default: function() {
        return [];
      }
    }
  },
  data() {
    return {
      destination: "",
      selected: null
    };
  },
  watch: {
    destination(dest) {
      this.$emit("destChange", dest);
    }
  },
  computed: {
    // TODO: This algorithm should really be imporoved
    filteredStops() {
      return this.stops.filter(option => {
        return (
          option
            .toString()
            .toLowerCase()
            .indexOf(this.destination.toLowerCase()) >= 0
        );
      });
    }
  }
};
</script>

<style>
</style>