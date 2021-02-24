<template>
  <v-dialog v-model="dialogVisibility" persistent width="600">
    <v-card>
      <v-simple-table fixed-header height="300px" class="mx-8">
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">
                No.
              </th>
              <th class="text-left">
                User
              </th>
              <th class="text-left">
                Points
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in userRanking" :key="index">
              <td>{{ index + 1 }}</td>
              <td>{{ item.userName }}</td>
              <td>{{ item.points }}</td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
      <v-card-actions>
        <v-spacer />
        <v-btn
          @click="closeDialog"
          width="100"
          class="mr-2 my-2"
          elevation="5"
          :color="theme.highlightColor"
        >
          <span class="button-label">CLOSE</span>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import { DefaultTheme } from "@/shared/constants";

export default {
  name: "UserRanking",
  data: () => ({
    theme: DefaultTheme
  }),
  props: {
    dialogVisibility: Boolean
  },
  computed: {
    ...mapGetters("feedModule", ["userRanking"])
  },
  methods: {
    ...mapActions("feedModule", ["getUserRanking"]),
    closeDialog() {
      this.$emit("closeDialog");
    }
  },
  async mounted() {
    await this.getUserRanking();
  }
};
</script>
