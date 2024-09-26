<template>
    <section v-if="vault" class="container-fluid bg-info">

        <div class="row text-center">
            <div class="col-12">
                <h1>Vault Details Page</h1>
                <h2>{{ vault.name }}</h2>
                <div>


                    <img :src="vault.img" :alt="vault.name" class="img-fluid rounded">

                </div>
                <div v-if="account.id == vault.creatorId">

                    <button @click="deleteVault(vault.id, vault.creatorId)" type="button"
                        class="btn btn-primary">DELETE</button>

                </div>
            </div>
        </div>

        <div v-if="keeps" class="row">

            <div class="masonry">
                <div v-for="keep in keeps" :key="keep.id" class="">
                    <VaultKeepComponent :keep="keep" :vaultKeepId="keep.vaultKeepId" />








                </div>
            </div>
        </div>
    </section>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { vaultKeepsService } from '../services/VaultKeepsService'


export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        async function getVaultKeepsByVaultId() {
            try {
                const vaultId = route.params.vaultId
                logger.log('fetching keeps', vaultId)

                await vaultKeepsService.getVaultKeepsByVaultId(vaultId)
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId

                await vaultsService.getVaultById(vaultId)
            }
            catch (error) {
                useRouter().push('/')
            }
        }


        onMounted(() => {
            getVaultKeepsByVaultId();
            getVaultById();
        })
        return {
            keeps: computed(() => AppState.vaultKeeps),
            vault: computed(() => AppState.activeVault),
            account: computed(() => AppState.account),

            async deleteVault(vaultId, profileId) {
                try {
                    const yes = await Pop.confirm()
                    if (!yes) return

                    await vaultsService.deleteVault(vaultId)
                    router.push({ name: 'Profile', params: { profileId: profileId } })
                }
                catch (error) {
                    Pop.error(error);
                }
            },


            // async deleteVaultKeep(vaultKeepId) {
            //     try {
            //         const yes = await Pop.confirm()
            //         if (!yes) return
            //         await vaultKeepsService.deleteVaultKeep(vaultKeepId)
            //     } catch (error) {
            //         Pop.error(error);
            //     }
            // }











        }

    }
}
</script>


<style lang="scss" scoped>
.img {
    // width: 100%;
    // object-fit: cover;
    height: 40vh;
}

.masonry {
    column-count: 3;
    /* Set the number of columns */
    column-gap: 1rem;
    /* Adjust gap between columns */


}

.masonry>div {
    break-inside: avoid;
    /* Prevent the item from breaking between columns */
    margin-bottom: 1rem;
    /* Add some spacing between items */
}

/* For tablets (medium screens), show 2 columns */
@media (max-width: 768px) {
    .masonry {
        column-count: 2;
        /* Reduce to 2 columns */
    }
}

/* For mobile phones (small screens), show 1 column */
@media (max-width: 576px) {
    .masonry {
        column-count: 1;
        /* Reduce to 1 column */
    }
}
</style>