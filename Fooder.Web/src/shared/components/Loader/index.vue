<template>
  <loading
    :active.sync="anyPendingRequests"
    :can-cancel="true"
    :z-index="9999"
  />
</template>

<script>
import { mapGetters } from "vuex";
import Loading from "vue-loading-overlay";

export default {
  components: {
    Loading
  },
  computed: {
    ...mapGetters("requestModule", ["anyPendingRequests"])
  }
};
</script>
