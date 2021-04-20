/* eslint-disable no-undef */
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId)
      AppState.profile = res.data
    } catch (err) {
      logger.error('Couldnt load Profiles', err)
    }
  }
}

export const profileService = new ProfileService()
